using UnityEngine;
using System.Collections;

public class GameEntity {

    private float _life;
    private float _velocity;
    private float _shield;
    //private Shot _shot;


    public float life
    {
        get { return _life; }
        set { _life = value; }
    }

    public float velocity
    {
        get { return _velocity; }
        set { _velocity = value; }
    }

    public float shield
    {
        get { return _shield; }
        set { _shield = value; }
    }

    //public float shot
    //{
    //    get { return _shot; }
    //    set { _shot = value; }
    //}

    public void shoot()
    {
    }

    public void move()
    {
    }

    public void proccess_damage(float damage)
    {
        life -= damage + shield;
        if (life <= 0)
        {
            Die();
        }

    }
    public void Die()
    {

    }
}
