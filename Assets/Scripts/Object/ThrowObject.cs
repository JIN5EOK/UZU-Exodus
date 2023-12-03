using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
    public void PickUp(Player player);
    public void Throw(Player player);
}

public class ThrowObject : MonoBehaviour, IThrowable
{
    private List<GameObject> hitEnemys = new List<GameObject>();
    

    [SerializeField]
    private float weight;

    [SerializeField] 
    private Rigidbody rigidBody;
    public void PickUp(Player player)
    {
        hitEnemys.Clear();
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = player.transform.position + Vector3.up * 2;
        transform.SetParent(player.transform);
    }
    public void Throw(Player player)
    {
        transform.position = player.transform.position;
        rigidBody.isKinematic = false;
        transform.SetParent(null);
        rigidBody.AddForce(player.transform.forward * 1000);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (rigidBody.velocity.magnitude < 1.0f)
            return;
        if (hitEnemys.Contains(other.gameObject))
            return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            hitEnemys.Add(other.gameObject);
            enemy.Hp--;
            Debug.Log("적과 충돌");
        }
    }
}