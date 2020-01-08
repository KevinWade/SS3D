using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS3D.Common {
    public interface IInteractionHandler {
        void Handle(Interaction interaction);
        bool CanHandle(Interaction interaction);
    }
}
