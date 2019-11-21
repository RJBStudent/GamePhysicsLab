using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class PhysicsPlugin
{
    [DllImport("PhysicsPlugin")]
    public static extern int InitParticleManager();
    [DllImport("PhysicsPlugin")]
    public static extern void UpdateParticleManager(float dt);
    [DllImport("PhysicsPlugin")]
    public static extern int TermParticleManager();
    [DllImport("PhysicsPlugin")]
    public static extern float[] getParticleValues(int key);
    [DllImport("PhysicsPlugin")]
    public static extern int AddNewParticle(float posX, float posY, float posZ, float rot, float mass);
    [DllImport("PhysicsPlugin")]
    public static extern void AddForce(float x, float y, int key);
    [DllImport("PhysicsPlugin")]
    public static extern void AddTorque(float value, int key);
    [DllImport("PhysicsPlugin")]
    public static extern float getParticlePosX(int key);
}
