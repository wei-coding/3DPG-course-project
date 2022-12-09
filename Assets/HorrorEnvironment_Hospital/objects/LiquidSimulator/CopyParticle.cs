using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyParticle : MonoBehaviour
{
    public GameObject particle;
    Vector3 original_pos;
    // Start is called before the first frame update
    void Start()
    {
        original_pos = particle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddMoreLiquid(Color color) {
        GameObject _particle = Instantiate(particle, original_pos, Quaternion.identity);
        _particle.GetComponent<SpriteRenderer>().color = color;
        for(int i=0; i<32; i++) {
            Instantiate(_particle,
                new Vector3(original_pos.x, original_pos.y + 0.02f * i, original_pos.z),
                Quaternion.identity);
        }
    }
}
