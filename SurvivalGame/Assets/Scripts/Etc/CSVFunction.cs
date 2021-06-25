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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "BodyDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    //public static void BodyInfoReader(string path, BodyDataInfo asset)
    public static void BodyDataInfoReader(BodyDataInfo asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("BodyDataInfo.csv");

        // 임의의 위치에 선택한 파일 읽기
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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "WheelDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    //public static void WheelDataInfoReader(string path, WheelDataInfo asset)
    public static void WheelDataInfoReader(WheelDataInfo asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("WheelDataInfo.csv");

        // 임의의 위치에 선택한 파일 읽기
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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "WeaponDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    //public static void WeaponDataInfoReader(string path, WeaponDataInfo asset)
    public static void WeaponDataInfoReader(WeaponDataInfo asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("WeaponDataInfo.csv");

        // 임의의 위치에 선택한 파일 읽기
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

    #region [CSV] Enemy Body Data Info 
    public static void EnemyBodyDataInfoWriter(BodyDataInfo asset)
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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "EnemyBodyDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public static void EnemyBodyDataInfoReader(BodyDataInfo asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("EnemyBodyDataInfo.csv");

        // 임의의 위치에 선택한 파일 읽기
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

    #region [CSV] Eenmy Wheel Data Info 
    public static void EnemyWheelDataInfoWriter(WheelDataInfo asset)
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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "EnemyWheelDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public static void EnemyWheelDataInfoReader(WheelDataInfo asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("EnemyWheelDataInfo.csv");

        // 임의의 위치에 선택한 파일 읽기
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

    #region [CSV] Enemy Weapon Data Info 
    public static void EnemyWeaponDataInfoWriter(WeaponDataInfo asset)
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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "EnemyWeaponDataInfo.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public static void EnemyWeaponDataInfoReader(WeaponDataInfo asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("EnemyWeaponDataInfo.csv");

        // 임의의 위치에 선택한 파일 읽기
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

    #region [CSV] Barrage Data
    public static void BarrageDataWriter(BarrageData asset)
    {
        List<string[]> data = new List<string[]>();

        string[] tempData = new string[7];
        tempData[0] = "key";
        tempData[1] = "eBarrageType";
        tempData[2] = "startAngle";
        tempData[3] = "addAngle";
        tempData[4] = "fireRunningTime";
        tempData[5] = "fireInterval";
        tempData[6] = "fireDelay";
        tempData[7] = "bulletCount";

        data.Add(tempData);

        for (int i = 0; i < asset.barrageDataList.Count; i++)
        {
            tempData = new string[6];
            tempData[0] = asset.barrageDataList[i].key;
            tempData[1] = asset.barrageDataList[i].eBarrageType.ToString();
            tempData[2] = asset.barrageDataList[i].startAngle.ToString();
            tempData[3] = asset.barrageDataList[i].addAngle.ToString();
            tempData[4] = asset.barrageDataList[i].fireRunningTime.ToString();
            tempData[5] = asset.barrageDataList[i].fireInterval.ToString();
            tempData[6] = asset.barrageDataList[i].fireDelay.ToString();
            tempData[7] = asset.barrageDataList[i].bulletCount.ToString();

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

        // 임의 경로
        string filePath = Application.dataPath + "/CSVFile/" + "BarrageData.csv";

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public static void BarrageDataReader(BarrageData asset)
    {
        // 지정된 위치의 파일 읽기
        List<Dictionary<string, object>> data = CSVReader.FileRead("BarrageData.csv");

        asset.barrageDataList.Clear();

        for (int i = 0; i < data.Count; i++)
            asset.barrageDataList.Add(new Barrage());

        for (int i = 0; i < data.Count; i++)
        {
            asset.barrageDataList[i].key = data[i]["key"].ToString();
            asset.barrageDataList[i].eBarrageType = (EBarrageType)Enum.Parse(typeof(EBarrageType), data[i]["eBarrageType"].ToString());
            asset.barrageDataList[i].startAngle = float.Parse(data[i]["startAngle"].ToString());
            asset.barrageDataList[i].addAngle = float.Parse(data[i]["addAngle"].ToString());
            asset.barrageDataList[i].fireRunningTime = float.Parse(data[i]["fireRunningTime"].ToString());
            asset.barrageDataList[i].fireInterval = float.Parse(data[i]["fireInterval"].ToString());
            asset.barrageDataList[i].fireDelay = float.Parse(data[i]["fireDelay"].ToString());
            asset.barrageDataList[i].bulletCount = int.Parse(data[i]["bulletCount"].ToString());
        }
    }
    #endregion
}
