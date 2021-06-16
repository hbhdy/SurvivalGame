using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class CSVFunction
{
    #region [CSV] Body Data Info 
    public static void BodyDataInfoWriter(BodyDataInfo asset)
    {
        List<string[]> data = new List<string[]>();

        string[] tempData = new string[4];
        tempData[0] = "itemCode";
        tempData[1] = "prefabName";
        tempData[2] = "HP";
        tempData[3] = "DEF";

        data.Add(tempData);

        for (int i = 0; i < asset.bodyDataList.Count; i++)
        {
            tempData = new string[6];
            tempData[0] = asset.bodyDataList[i].itemCode;
            tempData[1] = asset.bodyDataList[i].prefabName;
            tempData[2] = asset.bodyDataList[i].hp.ToString();
            tempData[3] = asset.bodyDataList[i].defence.ToString();

            data.Add(tempData);
        }

        string[][] output = new string[data.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = data[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        // ���� ���
        string filePath = Application.dataPath + "/CSVFile/" + "BodyDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    //public static void BodyInfoReader(string path, BodyDataInfo asset)
    public static void BodyDataInfoReader(BodyDataInfo asset)
    {
        // ������ ��ġ�� ���� �б�
        List<Dictionary<string, object>> data = CSVReader.FileRead("BodyDataInfo.csv");

        // ������ ��ġ�� ������ ���� �б�
        //List<Dictionary<string, object>> data = CSVReader.SearchRead(path);

        asset.bodyDataList.Clear();

        for (int i = 0; i < data.Count; i++)
            asset.bodyDataList.Add(new BodyData());

        for (int i = 0; i < data.Count; i++)
        {
            asset.bodyDataList[i].itemCode = data[i]["itemCode"].ToString();
            asset.bodyDataList[i].prefabName = data[i]["prefabName"].ToString();
            asset.bodyDataList[i].hp = int.Parse(data[i]["HP"].ToString());
            asset.bodyDataList[i].defence = int.Parse(data[i]["DEF"].ToString());
        }
    }
    #endregion

    #region [CSV] Wheel Data Info 
    public static void WheelDataInfoWriter(WheelDataInfo asset)
    {
        List<string[]> data = new List<string[]>();

        string[] tempData = new string[4];
        tempData[0] = "itemCode";
        tempData[1] = "prefabName";
        tempData[2] = "MovingSpeed";
        tempData[3] = "RotateSpeed";

        data.Add(tempData);

        for (int i = 0; i < asset.wheelDataList.Count; i++)
        {
            tempData = new string[6];
            tempData[0] = asset.wheelDataList[i].itemCode;
            tempData[1] = asset.wheelDataList[i].prefabName;
            tempData[2] = asset.wheelDataList[i].movingSpeed.ToString();
            tempData[3] = asset.wheelDataList[i].RotateSpeed.ToString();

            data.Add(tempData);
        }

        string[][] output = new string[data.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = data[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        // ���� ���
        string filePath = Application.dataPath + "/CSVFile/" + "WheelDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    //public static void WheelDataInfoReader(string path, WheelDataInfo asset)
    public static void WheelDataInfoReader(WheelDataInfo asset)
    {
        // ������ ��ġ�� ���� �б�
        List<Dictionary<string, object>> data = CSVReader.FileRead("WheelDataInfo.csv");

        // ������ ��ġ�� ������ ���� �б�
        //List<Dictionary<string, object>> data = CSVReader.SearchRead(path);

        asset.wheelDataList.Clear();

        for (int i = 0; i < data.Count; i++)
            asset.wheelDataList.Add(new WheelData());

        for (int i = 0; i < data.Count; i++)
        {
            asset.wheelDataList[i].itemCode = data[i]["itemCode"].ToString();
            asset.wheelDataList[i].prefabName = data[i]["prefabName"].ToString();
            asset.wheelDataList[i].movingSpeed = float.Parse(data[i]["MovingSpeed"].ToString());
            asset.wheelDataList[i].RotateSpeed = float.Parse(data[i]["RotateSpeed"].ToString());
        }
    }
    #endregion

    #region [CSV] Weapon Data Info 
    public static void WeaponDataInfoWriter(WeaponDataInfo asset)
    {
        List<string[]> data = new List<string[]>();

        string[] tempData = new string[5];
        tempData[0] = "itemCode";
        tempData[1] = "prefabName";
        tempData[2] = "attackDamage";
        tempData[3] = "criticalChance";
        tempData[4] = "criticalDamage";

        data.Add(tempData);

        for (int i = 0; i < asset.weaponDataList.Count; i++)
        {
            tempData = new string[6];
            tempData[0] = asset.weaponDataList[i].itemCode;
            tempData[1] = asset.weaponDataList[i].prefabName;
            tempData[2] = asset.weaponDataList[i].attackDamage.ToString();
            tempData[3] = asset.weaponDataList[i].criChance.ToString();
            tempData[4] = asset.weaponDataList[i].criDamage.ToString();

            data.Add(tempData);
        }

        string[][] output = new string[data.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = data[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        // ���� ���
        string filePath = Application.dataPath + "/CSVFile/" + "WeaponDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    //public static void WeaponDataInfoReader(string path, WeaponDataInfo asset)
    public static void WeaponDataInfoReader(WeaponDataInfo asset)
    {
        // ������ ��ġ�� ���� �б�
        List<Dictionary<string, object>> data = CSVReader.FileRead("WeaponDataInfo.csv");

        // ������ ��ġ�� ������ ���� �б�
        //List<Dictionary<string, object>> data = CSVReader.SearchRead(path);

        asset.weaponDataList.Clear();

        for (int i = 0; i < data.Count; i++)
            asset.weaponDataList.Add(new WeaponData());

        for (int i = 0; i < data.Count; i++)
        {
            asset.weaponDataList[i].itemCode = data[i]["itemCode"].ToString();
            asset.weaponDataList[i].prefabName = data[i]["prefabName"].ToString();
            asset.weaponDataList[i].attackDamage = float.Parse(data[i]["attackDamage"].ToString());
            asset.weaponDataList[i].criChance = float.Parse(data[i]["criticalChance"].ToString());
            asset.weaponDataList[i].criDamage = float.Parse(data[i]["criticalDamage"].ToString());
        }
    }
    #endregion
}
