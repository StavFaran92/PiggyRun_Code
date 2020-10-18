using System;

namespace Assets.Scripts.SystemAI
{
    internal class GameEventRules
    {
        internal static bool IsValidCombination(GameEvent gameEventType1, GameEvent gameEventType2, AttachmentType attachment)
        {
            return true;
            //return gameEventType1.GetAttachmentOptions(attachment).Contains(gameEventType2.GetEventType());
        }
    }
}