using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMovementSystem
{
    public class MoveToCommand : ICommand, ICommandListener
    {
        private Vector3 _targetPosition;
        private MoveObject _objectToMove;
        private bool isFinished = false;
        public bool IsFinished => isFinished;

        public MoveToCommand(Vector3 targetPosition, MoveObject objectToMove)
        {
            _targetPosition = targetPosition;
            _objectToMove = objectToMove;
        }

        public void Execute()
        {
            isFinished = false;
            _objectToMove.MoveTo(_targetPosition, this);
        }

        public void OnCommandCompleted()
        {
            isFinished = true;
        }
    }
}