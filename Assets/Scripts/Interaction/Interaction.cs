using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS3D.Common {
    public struct Interaction {
        public Intent Intent { get; set; }
        // The player or system that initiated the interaction
        public InteractableBehaviour Initiator { get; set; }
        // The thing actually doing the interacting
        public InteractableBehaviour Interactor { get; set; }
        public InteractableBehaviour Target { get; set; }

        public void Execute() {
            Initiator.CmdExecuteInteraction(this);
        }
    }
}

