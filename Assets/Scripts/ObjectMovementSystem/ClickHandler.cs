using System;
using UnityEngine;

namespace ObjectMovementSystem
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private MoveObject objectToMove;
        private CommandManager _commandManager;

        private void Start()
        {
            _commandManager = CommandManager.Instance;
            //фиксируем в истории изначальное положение объекта
            ICommand moveCommand = new MoveToCommand(objectToMove.transform.position, objectToMove);
            _commandManager.AddCommand(moveCommand);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.z = objectToMove.transform.position.z;
                ICommand moveCommand = new MoveToCommand(clickPosition, objectToMove);
                _commandManager.AddCommand(moveCommand);
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                _commandManager.Undo();
            }
            
            _commandManager.ExecuteNextCommand();
        }
    }
}
