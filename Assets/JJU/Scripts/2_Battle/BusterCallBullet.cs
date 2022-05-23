using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterCallBullet : MonoBehaviour
{
    public Vector3 targetPos;
    public AudioSource audioSource;
    public Unit instigator;


    void Update()
    {
        if(transform.position == targetPos)
        {
            string ran = Random.Range(1, 5).ToString();
            var explosion = Instantiate(GameInstance.Instance.particlePrefab["BulletExplosive" + ran],transform.position,Quaternion.identity);
            audioSource.PlayOneShot(GameInstance.Instance.soundPrefab["Bomb"]);

            foreach (Enemy enemy in BattleManager.I.enemyCollection.collection)
            {
                enemy.unit.DecreaseHP(instigator.currentPhysicalAttack * 0.2f, instigator);
                //var particle = Instantiate(GameInstance.Instance.particlePrefab["Hit_1"], enemy.transform.position + Vector3.up * 1.5f, Quaternion.identity);
                //Destroy(particle, 0.5f);
            }
            Destroy(explosion, 0.5f);
            Destroy(gameObject);
        }
    }
}
