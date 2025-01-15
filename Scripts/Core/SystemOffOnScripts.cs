using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RAY_Core
{
    public class SystemOffOnScripts : MonoBehaviour
    {
        [SerializeField] private string PathFolderScripts;
        [SerializeField] private PairScript[] PairsScripts;

        [Button]
        public void RefreshList()
        {
            if (Directory.Exists(PathFolderScripts))
            {
                var array = Directory.GetFiles(PathFolderScripts, "*.cs", SearchOption.AllDirectories);

                PairsScripts = new PairScript[array.Length];

                Debug.Log("Путь существует");

                int index = 0;

                foreach (var s in array)
                {
                    using StreamReader sr = new(s);

                    string temp = sr.ReadLine();

                    if (temp == null)
                    {
                        continue;
                    }

                    bool flag = temp.Split(' ')[0] != "#if" || temp.Split(' ')[1][0] == '!';

                    PairsScripts[index] = new()
                    {
                        FullPath = s,
                        NameScript = Path.GetFileName(s),
                        IsEnable = flag,
                        IsSendCommand = false,
                    };

                    Debug.Log("Прочитан файл " + PairsScripts[index].NameScript);

                    index++;
                }
            }    
        }
        [Button]
        public void ExecuteCommands()
        {
            for (int i = 0; i < PairsScripts.Length; i++)
            {
                if (PairsScripts[i].IsSendCommand)
                {
                    Debug.Log("Команда активна");

                    if (File.Exists(PairsScripts[i].FullPath))
                    {
                        Debug.Log(PairsScripts[i].NameScript + " файл существует");

                        using FileStream fstream = new(PairsScripts[i].FullPath, FileMode.Open);

                        byte[] buffer = Encoding.Default.GetBytes(PairsScripts[i].IsEnable ? "#if ENABLE_ERRORS" : "#if !ENABLE_ERRORS");

                        fstream.Write(buffer, 0, buffer.Length);

                        PairsScripts[i].IsEnable = !PairsScripts[i].IsEnable;
                        PairsScripts[i].IsSendCommand = false;

                        Debug.Log("Файл изменён");
                    }
                }

                Debug.Log("Прочитан файл " + PairsScripts[i].NameScript);
            }
        }
        [Button]
        public void ClearList()
        {
            PairsScripts = new PairScript[0];
        }
    }
    [Serializable]
    public struct PairScript
    {
        [SerializeField][ReadOnly] public string NameScript;
        [SerializeField][ReadOnly] public string FullPath;
        [SerializeField][ReadOnly] public bool IsEnable;
        [SerializeField] public bool IsSendCommand;
    }
}
