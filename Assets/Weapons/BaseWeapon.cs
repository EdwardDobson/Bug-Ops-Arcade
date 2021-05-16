using UnityEngine;

public enum GunTypes
{
    eRifle,
    ePistol,
    eLaser,
    eGrenadeLauncher,
    eShotgun,

}

[CreateAssetMenu(fileName = "BaseWeapon", menuName = "Weapons", order = 1)]
public class BaseWeapon : ScriptableObject
{
    public string WeaponName;
    public Sprite WeaponIcon;
    public Sprite BulletSprite;
    public Sprite Mag;
    public GunTypes GunType;
    public float BulletMoveSpeed;
    public float GunFireRate;
    public int AmmoCapMax;
    public int AmmoCapCurrent;
    public float ReloadTime;
    public float Damage;
    public float AOE;
    public float BulletAliveTime;
    public float LaserRange;
    public int BulletAmount;
}
