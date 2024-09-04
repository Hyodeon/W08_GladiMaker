using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WangKalManEffect : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnLocations;
    [SerializeField] GameObject SmashEffect;
    float scales = 1f;

    [SerializeField] float spawnDelay = .2f;

    public void Start_Spawn_Smash_Effect()
    {
        StartCoroutine(Spawn_Smash_Effect());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GetComponent<Animator>().Play("SkillAttack");
    }

    IEnumerator Spawn_Smash_Effect()
    {
        for (int i = 0; i < spawnLocations.Length; i++) {
            var smash = Instantiate(SmashEffect, new Vector2(spawnLocations[i].position.x, spawnLocations[i].position.y + 1.7f), Quaternion.identity);
            Destroy(smash, .5f);
            smash.transform.localScale = new Vector2(scales, scales);
            scales += i == spawnLocations.Length - 1 ? 10 : .5f;
            yield return new WaitForSeconds(spawnDelay);
        }
        scales = 1;
    }
}
