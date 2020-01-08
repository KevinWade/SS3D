using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace SS3D.Common {
    [DisallowMultipleComponent]
    public class InteractableBehaviour : NetworkBehaviour {
        private Dictionary<Intent, List<IInteractionHandler>> interactorHandlers = new Dictionary<Intent, List<IInteractionHandler>>();
        private Dictionary<Intent, List<IInteractionHandler>> targetHandlers = new Dictionary<Intent, List<IInteractionHandler>>();

        public void AddInteractorHandler(Intent intent, IInteractionHandler handler) {
            AddHandler(intent, handler, interactorHandlers);
        }

        public void AddTargetHandler(Intent intent, IInteractionHandler handler) {
            AddHandler(intent, handler, targetHandlers);
        }

      
        private void AddHandler(Intent intent, IInteractionHandler handler, Dictionary<Intent, List<IInteractionHandler>> handlers) {
            if (!handlers.ContainsKey(intent)) {
                handlers.Add(intent, new List<IInteractionHandler>());
            }
            handlers[intent].Add(handler);
        }

        private IInteractionHandler GetTargetHandler(Interaction interaction) {
            return GetHandler(interaction, targetHandlers);
        }

        private IInteractionHandler GetInteractorHandler(Interaction interaction) {
            return GetHandler(interaction, interactorHandlers);
        }

        private IInteractionHandler GetHandler(Interaction interaction, Dictionary<Intent, List<IInteractionHandler>> handlers) {
            if (!handlers.ContainsKey(interaction.Intent)) {
                return null;
            }
            for (int i = 0; i < handlers[interaction.Intent].Count; i++) {
                if (handlers[interaction.Intent][i].CanHandle(interaction)) {
                    return handlers[interaction.Intent][i];
                }
            }
            return null;
        }

        [Command]
        public void CmdExecuteInteraction(Interaction interaction) {
            IInteractionHandler interactorHandler = interaction.Interactor.GetInteractorHandler(interaction);
            IInteractionHandler targetHandler = interaction.Target.GetTargetHandler(interaction);
            if (interactorHandler != null) {
                interactorHandler.Handle(interaction);
            }
            if (targetHandler != null) {
                targetHandler.Handle(interaction);
            }
        }
    }
}

