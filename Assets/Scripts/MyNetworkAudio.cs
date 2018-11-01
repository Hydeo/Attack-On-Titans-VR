﻿namespace PlayoVR {
    using UnityEngine;
    using NetBase;

    public class MyNetworkAudio : NetworkAudio {

        protected override int GetClipId(AudioClip clip) {
            return AudioManager.GetClipId(clip);
        }

        protected override int GetClipId(string clipName) {
            return AudioManager.GetClipId(clipName);
        }

        protected override AudioClip GetClip(int clipId) {
            return AudioManager.GetClip(clipId);
        }

    }
}
