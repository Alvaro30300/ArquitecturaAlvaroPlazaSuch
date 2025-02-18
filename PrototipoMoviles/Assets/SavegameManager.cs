﻿using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SavegameManager : MonoBehaviour
{
    public static SavegameManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    //public Enemy[] enemies;

    //private void Start()
    //{
    //    string message = "Hello World";
    //    byte[] encryptedMessage = Encrypt(message);
    //    string decryptedMessage = Decrypt(encryptedMessage);

    //    Debug.Log(decryptedMessage);
    //}

    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        
    }

    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};
    byte[] _inicializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    byte[] Encrypt(string message)
    {
        AesManaged aes = new AesManaged();
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _inicializationVector);

        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        streamWriter.WriteLine(message);

        streamWriter.Close();
        cryptoStream.Close();
        memoryStream.Close();

        return memoryStream.ToArray();
    }

    string Decrypt(byte[] message)
    {
        AesManaged aes = new AesManaged();
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _inicializationVector);

        MemoryStream memoryStream = new MemoryStream(message);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        StreamReader streamReader = new StreamReader(cryptoStream);

        string decryptedMessage = streamReader.ReadToEnd();

        memoryStream.Close();
        cryptoStream.Close();
        streamReader.Close();

        return decryptedMessage;
    }
    public int Load()
    {

            string filePath = Application.persistentDataPath + "/savegame.sav";
            Debug.Log("Loading from: " + filePath);
            byte[] decryptedMessage = File.ReadAllBytes(filePath);
            string jsonString = Decrypt(decryptedMessage);
            JObject jSaveGame = JObject.Parse(jsonString);

        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    Enemy curEnemy = enemies[i];
        //    string enemyJsonString = jSaveGame[curEnemy.name].ToString();
        //    curEnemy.Deserialize(enemyJsonString);
        //}
        return int.Parse(jSaveGame["score"].ToString());
    }
    public void Save(int score)
    {

            JObject jSaveGame = new JObject();
        jSaveGame.Add("score",score);

            //for (int i = 0; i < enemies.Length; i++)
            //{
            //    //Enemy curEnemy = enemies[i];
            //    JObject serializedEnemy = curEnemy.Serialize();
            //    jSaveGame.Add(curEnemy.name, serializedEnemy);
            //}
            string filePath = Application.persistentDataPath + "/savegame.sav";
            byte[] encryptedMessage = Encrypt(jSaveGame.ToString());
            File.WriteAllBytes(filePath, encryptedMessage);
        Debug.Log("Saved in: " + filePath);


    }

}
