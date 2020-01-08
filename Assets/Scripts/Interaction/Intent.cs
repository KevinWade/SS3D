using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace SS3D.Common {
    public class Intent {
        public static readonly Intent HELP = new Intent(1);
        public static readonly Intent HARM = new Intent(2);
        public static readonly Intent GRAB = new Intent(3);

        public int Value { get; private set; }

        private Intent(int value) {
            Value = value;
        }

        public static explicit operator Intent(int v) {
            FieldInfo[] fields = typeof(Intent).GetFields();
            for (int i = 0; i < fields.Length; i++) {
                if (fields[i].IsStatic && fields[i].FieldType == typeof(Intent)) {
                    Intent verb = (Intent)fields[i].GetValue(null);
                    if (verb.Value == v) {
                        return verb;
                    }
                }
            }
            return null;
        }
    }
}
