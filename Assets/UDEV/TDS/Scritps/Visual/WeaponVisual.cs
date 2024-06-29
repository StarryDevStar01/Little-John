using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private AudioClip m_shootingSound;
    [SerializeField] private AudioClip m_reloadSound;

    public void OnShoot()
    {
        AudioController.Ins.PlaySound(m_shootingSound);

        CineController.Ins.ShakeTrigger();
    }

    public void OnReload()
    {
        GUIManager.Ins.ShowReloadTxt(true);
    }

    public void OnReloadDone()
    {
        AudioController.Ins.PlaySound(m_reloadSound);

        GUIManager.Ins.ShowReloadTxt(false);
    }
}
