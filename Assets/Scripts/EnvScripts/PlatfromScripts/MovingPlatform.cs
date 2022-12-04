﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OrkWizard
{
    public class MovingPlatform : PlatformHandler
    {
        private bool shouldMove;
        private bool reset = false;
        private bool reverseMode = false;
        private bool finishedOnce = false;
        private Vector2 originalPossition;
        private int possitionToMoveIndex;

        [SerializeField]
        private List<Vector2> possitionsToMove;

        [SerializeField]
        protected MovingPlatformSO movingPlatformSO;

        protected override void Initialize()
        {
            base.Initialize();
            originalPossition = transform.position;
            possitionsToMove.Add(originalPossition);
            possitionToMoveIndex = 0;
            shouldMove = !movingPlatformSO.playerTriggerMovement;
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (reset)
            {
                Reset();
                return;
            }

            if (NeedsToMove())
            {
                Vector2 possition = FindPossitionToMove();
                MoveToPossition(possition);
            }
        }

        private bool NeedsToMove()
        {
            if (movingPlatformSO.finishOneTime && finishedOnce)
            {
                return false;
            }

            // Contains any possitions to move, and not falling platform
            if (possitionsToMove != null && possitionToMoveIndex < possitionsToMove.Count)
            {
                // This gets set by player interaction if needed;
                return shouldMove;
            }

            return false;
        }

        private void Reset()
        {
            if ((Vector2)transform.position != originalPossition)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPossition, movingPlatformSO.platformSpeed * Time.deltaTime);
            }
            else
            {
                reset = false;
                shouldMove = false;
                finishedOnce = false;
                reverseMode = false;
                possitionToMoveIndex = 0;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                boxCollider.enabled = true;
                transform.position = originalPossition;
            }
        }

        private Vector2 FindPossitionToMove()
        {
            if ((Vector2)transform.position == possitionsToMove[possitionToMoveIndex])
            {

                if (movingPlatformSO.stopTime != 0)
                {
                    shouldMove = false;
                    Invoke("ShouldMoveAfterStop", movingPlatformSO.stopTime);
                }

                if (movingPlatformSO.platformType == PlatformMovementType.Moving)
                {
                    possitionToMoveIndex = FindIndexForForwardMovement();
                }
                else if (movingPlatformSO.platformType == PlatformMovementType.ReverseOrderOnFinish)
                {
                    possitionToMoveIndex = FindIndexForReverseMovement();
                }
            }
            return possitionsToMove[possitionToMoveIndex];
        }

        // Two ugly functions. Can we do better?
        private int FindIndexForForwardMovement()
        {
            var returnIndex = 0;

            if (possitionToMoveIndex + 1 >= possitionsToMove.Count)
            {
                finishedOnce = true;
            }
            else
            {
                returnIndex = possitionToMoveIndex + 1;
            }

            return returnIndex;
        }

        private int FindIndexForReverseMovement()
        {
            var returnInt = 0;

            if (!reverseMode)
            {
                if (possitionToMoveIndex + 1 >= possitionsToMove.Count)
                {
                    reverseMode = true;
                    finishedOnce = true;
                    returnInt = possitionToMoveIndex - 1;
                }
                else
                {
                    returnInt = possitionToMoveIndex + 1;
                }
            }
            else
            {
                if (possitionToMoveIndex == 0)
                {
                    reverseMode = false;
                    returnInt = possitionToMoveIndex + 1;
                }
                else
                {
                    returnInt = possitionToMoveIndex - 1;
                }
            }

            return returnInt;
        }

        private void MoveToPossition(Vector2 possition)
        {
            transform.position = Vector2.MoveTowards(transform.position, possition, movingPlatformSO.platformSpeed * Time.deltaTime);
        }

        private void ShouldMoveAfterStop()
        {
            shouldMove = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_playerTag) && movingPlatformSO.playerTriggerMovement)
            {
                if (collision.gameObject.CompareTag(_playerTag) && movingPlatformSO.playerTriggerMovement)
                {
                    shouldMove = true;
                    reset = false;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_playerTag) && movingPlatformSO.playerTriggerMovement)
            {
                reset = true;
            }
        }

        private void OnDrawGizmos()
        {
            var number = 0;
            var boxColliderGizmos = GetComponent<BoxCollider2D>();
            foreach (var possition in possitionsToMove)
            {
                Gizmos.color = Color.white;
                //Handles.color = Color.white;
                //Handles.Label(possition, $"Possition {number}");
                Gizmos.DrawWireCube(possition, new Vector2(boxColliderGizmos.size.x, boxColliderGizmos.size.y));
                number++;
            }
        }

    }
}
