using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool : MonoBehaviour
{
    // Instancia única del Pool (Patrón Singleton)
    public static GenericPool Instance;

    // Prefab del objeto que se va a instanciar (en este caso, una bala)
    public GameObject bulletPrefab;

    // Tamaño inicial del pool
    public int poolSize = 20;

    // Cola para almacenar los objetos disponibles en el pool
    private Queue<GameObject> bulletQueue = new Queue<GameObject>();

    private void Awake()
    {
        // Se asigna la instancia para usar el Singleton
        Instance = this;
        // Se inicializa el pool de balas
        InitializePool();
    }

    private void InitializePool()
    {
        // Se crean y almacenan las balas en el pool según el tamaño definido
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab); // Se instancia una nueva bala
            bullet.SetActive(false); // Se desactiva para que no esté en escena hasta que sea requerida
            bulletQueue.Enqueue(bullet); // Se almacena en la cola del pool
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet;

        // Verifica si hay balas disponibles en el pool
        if (bulletQueue.Count > 0)
        {
            bullet = bulletQueue.Dequeue(); // Obtiene una bala del pool
        }
        else
        {
            // Si el pool está vacío, se instancia una nueva bala
            bullet = Instantiate(bulletPrefab);
        }

        // Se posiciona y rota la bala según los parámetros de entrada
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true); // Se activa para que aparezca en la escena

        return bullet; // Se devuelve la referencia a la bala
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false); // Se desactiva la bala para que no esté en escena
        bulletQueue.Enqueue(bullet); // Se devuelve al pool para ser reutilizada
    }
}