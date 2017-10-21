using Door_of_Soul.Communication.HexagramNodeServer;
using Door_of_Soul.Communication.HexagramNodeServer.Throne.Device;
using Door_of_Soul.Communication.HexagramNodeServer.Throne;
using Door_of_Soul.Communication.Protocol.Hexagram.Throne.Device;
using Door_of_Soul.Core;
using Door_of_Soul.Core.HexagramNodeServer;
using Door_of_Soul.Core.Protocol;
using Door_of_Soul.Database.Repository.Throne;
using System.Collections.Generic;
using System;

namespace Door_of_Soul.HexagramThroneServer
{
    class HexagramThrone : VirtualThrone
    {
        private HashSet<int> waitingForAssignAnswerIdSet = new HashSet<int>();

        private object getAnswerTrinityServerLock = new object();
        private object assignAnswerLock = new object();

        private int HexagramEntranceIdForAssignAnswer
        {
            get
            {
                int minAccessAnswerCount = -1;
                int minAccessAnswerEntranceId = 0;
                foreach (var entrance in ThroneHexagramEntranceFactory.Instance.Subjects)
                {
                    if(entrance.AccessAnswerCount > minAccessAnswerCount)
                    {
                        minAccessAnswerCount = entrance.AccessAnswerCount;
                        minAccessAnswerEntranceId = entrance.HexagramEntranceId;
                    }
                }
                return minAccessAnswerEntranceId;
            }
        }

        public override string ToString()
        {
            return $"Hexagram{base.ToString()}";
        }

        public override OperationReturnCode DeviceRegister(int entranceId, int endPointId, int deviceId, string answerName, string basicPassword, out string errorMessage)
        {
            int answerId;
            OperationReturnCode returnCode = AnswerRepository.Instance.Register(answerName, basicPassword, out errorMessage, out answerId);
            ThroneHexagramEntrance entrance;
            if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out entrance))
            {
                DeviceThroneOperationResponseApi.SendOperationResponse(entrance, endPointId, deviceId, DeviceThroneOperationCode.Register, returnCode, errorMessage, new System.Collections.Generic.Dictionary<byte, object>());
            }
            else
            {
                errorMessage = $"DeviceRegister Failed ThroneHexagramEntranceId:{entranceId} not existed";
                returnCode = OperationReturnCode.NotExisted;
            }
            return returnCode;
        }

        public override OperationReturnCode GetAnswerTrinityServer(int entranceId, int answerId, out string errorMessage)
        {
            errorMessage = "";
            OperationReturnCode returnCode = OperationReturnCode.Successiful;
            lock (getAnswerTrinityServerLock)
            {
                ThroneAnswer answer;
                if (AnswerFactory.Instance.Find(answerId, out answer))
                {
                    ThroneHexagramEntrance entrance;
                    if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out entrance))
                    {
                        ThroneOperationResponseApi.GetAnswerTrinityServer(
                            terminal: entrance,
                            returnCode: returnCode,
                            operationMessage: errorMessage,
                            trinityServerEndPointId: answer.AccessEndPointId,
                            answerId: answerId,
                            answerAccessToken: answer.AnswerAccessToken);
                    }
                    else
                    {
                        errorMessage = $"GetAnswerTrinityServer Failed HexagramEntranceId:{entranceId} not existed";
                        returnCode = OperationReturnCode.NotExisted;
                    }
                }
                else
                {
                    ThroneHexagramEntrance dependentEntrance;
                    List<IEventDependencyReleasable> eventDependentTargets = new List<IEventDependencyReleasable>();
                    if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out dependentEntrance))
                    {
                        eventDependentTargets.Add(dependentEntrance);
                    }
                    OnAssignAnswer.RegisterEvent(
                        eventHandler: (callbackSubject, eventParameter) =>
                        {
                            if (eventParameter.answerId != answerId)
                                return false;
                            if(waitingForAssignAnswerIdSet.Contains(eventParameter.answerId))
                            {
                                ThroneHexagramEntrance entrance;
                                if (ThroneHexagramEntranceFactory.Instance.Find(entranceId, out entrance))
                                {
                                    ThroneOperationResponseApi.GetAnswerTrinityServer(
                                        terminal: entrance,
                                        returnCode: eventParameter.returnCode,
                                        operationMessage: eventParameter.operationMessage,
                                        trinityServerEndPointId: eventParameter.trinityServerEndPointId,
                                        answerId: eventParameter.answerId,
                                        answerAccessToken: eventParameter.answerAccessToken);
                                }
                                waitingForAssignAnswerIdSet.Remove(eventParameter.answerId);
                            }
                            return true;
                        },
                        dependentTargets: eventDependentTargets);
                    returnCode = AssignAnswer(answerId, out errorMessage);
                }
            }
            return returnCode;
        }

        public override void AssignAnswerResponse(AssignAnswerResponseParameter responseParameter)
        {
            lock (getAnswerTrinityServerLock)
            {
                lock(assignAnswerLock)
                {
                    OnAssignAnswer.InvokeEvent(responseParameter);
                }
            }
        }

        public override OperationReturnCode AssignAnswer(int answerId, out string errorMessage)
        {
            lock (assignAnswerLock)
            {
                if (waitingForAssignAnswerIdSet.Contains(answerId))
                {
                    errorMessage = "";
                    return OperationReturnCode.Successiful;
                }
                else
                {
                    waitingForAssignAnswerIdSet.Add(answerId);
                    int targetEntranceId = HexagramEntranceIdForAssignAnswer;
                    ThroneHexagramEntrance targetEntrance;
                    if (ThroneHexagramEntranceFactory.Instance.Find(targetEntranceId, out targetEntrance))
                    {
                        targetEntrance.OnDisconnected += () =>
                        {
                            AssignAnswerResponse(new AssignAnswerResponseParameter
                            {
                                returnCode = OperationReturnCode.TargetDisconnected,
                                operationMessage = $"AssignAnswer target HexagramEntranceId:{targetEntranceId} disconnected",
                            });
                        };

                        targetEntrance.IncreaseAccessAnswerCount();
                        ThroneInverseOperationRequestApi.AssignAnswer(targetEntrance, answerId);
                        errorMessage = "";
                        return OperationReturnCode.Successiful;
                    }
                    else
                    {
                        errorMessage = $"ThroneHexagramEntrance Id:{targetEntranceId} not existed";
                        return OperationReturnCode.NotExisted;
                    }
                }
            }
        }
    }
}
