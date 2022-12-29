using UnityEngine;

namespace CCintron.PianoDemo
{
    public class PianoDemo : MonoBehaviour
    {
        void Awake()
        {
            SoundLibrary mSoundLibrary = new SoundLibrary();
            SoundManager mSoundManager = new SoundManager();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) PlaySound(SoundEffectType.Sound_01);
            if (Input.GetKeyDown(KeyCode.S)) PlaySound(SoundEffectType.Sound_02);
            if (Input.GetKeyDown(KeyCode.D)) PlaySound(SoundEffectType.Sound_03);
            if (Input.GetKeyDown(KeyCode.F)) PlaySound(SoundEffectType.Sound_04);
            if (Input.GetKeyDown(KeyCode.J)) PlaySound(SoundEffectType.Sound_05);
            if (Input.GetKeyDown(KeyCode.K)) PlaySound(SoundEffectType.Sound_06);
            if (Input.GetKeyDown(KeyCode.L)) PlaySound(SoundEffectType.Sound_07);
        }

        void PlaySound(SoundEffectType soundEffectType)
        {
            SoundManager.Add(soundEffectType);
        }
    }
}