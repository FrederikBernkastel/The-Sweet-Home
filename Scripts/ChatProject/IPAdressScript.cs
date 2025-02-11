#if ENABLE_ERRORS

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PracticeProject_Lesson7
{
    public class IPAdressScript : MonoBehaviour
    {
        public TMP_Text PrefabText;
        public Transform RefStorage;
        public Button RefButton;
        public TMP_InputField RefInputField;

        private StreamReader Reader;
        private StreamWriter Writer;

        private string NameUser;


        // Start is called before the first frame update
        void Start()
        {
            var text = GameObject.Instantiate(PrefabText, RefStorage);

            text.text = "Система: Введите IP адресс чата...";

            RefButton.onClick.AddListener(ClickButtonIPAdress);
        }
        private async void Update()
        {
            if (Reader != null)
            {
                string? message = await Reader.ReadLineAsync();

                if (!string.IsNullOrEmpty(message))
                {
                    var text = GameObject.Instantiate(PrefabText, RefStorage);

                    text.text = message;
                }
            }
        }
        private void ClickButtonIPAdress()
        {
            if (IPAddress.TryParse(RefInputField.text, out var ipAd))
            {
                var client = new TcpClient();
                client.Connect(new IPEndPoint(ipAd, 8888));

                if (client.Connected)
                {
                    Reader = new StreamReader(client.GetStream());
                    Writer = new StreamWriter(client.GetStream());

                    var text = GameObject.Instantiate(PrefabText, RefStorage);

                    text.text = "Система: Подключение успешно...";

                    text = GameObject.Instantiate(PrefabText, RefStorage);

                    text.text = "Система: Введите никнейм...";

                    RefButton.onClick.RemoveListener(ClickButtonIPAdress);

                    RefButton.onClick.AddListener(ClickButtonNameUser);
                }
                else
                {
                    var text = GameObject.Instantiate(PrefabText, RefStorage);

                    text.text = "Система: Не удалось подключить к данному чату...";

                    text = GameObject.Instantiate(PrefabText, RefStorage);

                    text.text = "Система: Введите IP адресс чата...";
                }
            }
            else
            {
                var text = GameObject.Instantiate(PrefabText, RefStorage);

                text.text = "Система: Неккоректный IP, попробуйте ещё раз...";

                text = GameObject.Instantiate(PrefabText, RefStorage);

                text.text = "Система: Введите IP адресс чата...";
            }
        }
        private async void ClickButtonNameUser()
        {
            await Writer.WriteLineAsync(RefInputField.text);
            await Writer.FlushAsync();

            var text = GameObject.Instantiate(PrefabText, RefStorage);

            NameUser = RefInputField.text;

            text.text = "Система: Добро пожаловать " + NameUser;

            text = GameObject.Instantiate(PrefabText, RefStorage);

            text.text = "Система: Введите сообщение и нажмите [ENTER]...";

            RefButton.onClick.RemoveListener(ClickButtonNameUser);

            RefButton.onClick.AddListener(SendMessageAsync);
        }
        private void OnDestroy()
        {
            Writer?.Close();
            Reader?.Close();
        }
        private async void SendMessageAsync()
        {
            await Writer.WriteLineAsync(RefInputField.text);
            await Writer.FlushAsync();

            var text = GameObject.Instantiate(PrefabText, RefStorage);

            text.text = NameUser + ": " + RefInputField.text;
        }
    }
}
#endif