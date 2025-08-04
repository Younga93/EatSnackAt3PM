//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ParticleOutfitItem : OutfitItemBase
//{
//    private GameObject particleInstance;
//    public ParticleOutfitItem(int id, string name, int price, string imageFileName) : base(id, name, price, imageFileName)
//    {
//    }
//    public override void ApplyOutfitItem(PlayerController player)
//    {
//        if (particleInstance != null) return; // 이미 생성되어 있으면 패스

//        particleInstance = new GameObject($"Particle_{Name}");
//        particleInstance.transform.SetParent(player.transform);
//        particleInstance.transform.localPosition = Vector3.zero;

//        ParticleSystem ps = particleInstance.AddComponent<ParticleSystem>();
//        ps.Play();

//    }
//    public void UnEquipOutfitItem(PlayerController player)
//    {
//        if (particleInstance != null)
//        {
//            GameObject.Destroy(particleInstance);
//            particleInstance = null;
//        }
//    }
//}
