import * as signalR from "@microsoft/signalr";

const txtUsername: HTMLInputElement = document.getElementById(
  "txtUsername"
) as HTMLInputElement;
const txtMessage: HTMLInputElement = document.getElementById(
  "txtMessage"
) as HTMLInputElement;
const btnSend: HTMLButtonElement = document.getElementById(
  "btnSend"
) as HTMLButtonElement;

btnSend.disabled = true;

const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7159/chatHub")
  .build();

connection.on("ReceiveMessage", (username: string, message: string) => {
  const li = document.createElement("li");
  li.textContent = `${username}: ${message}`;
  const messageList = document.getElementById("messages");
  messageList.appendChild(li);
  messageList.scrollTop = messageList.scrollHeight;
});

connection
  .start()
  .then(() => (btnSend.disabled = false))
  .catch((err) => console.error(err.toString()));

txtMessage.addEventListener("keyup", (event) => {
  if (event.key === "Enter") {
    sendMessage();
  }
});

btnSend.addEventListener("click", sendMessage);

function sendMessage() {
  connection
    .invoke("SendMessage", txtUsername.value, txtMessage.value)
    .catch((err) => console.error(err.toString()))
    .then(() => (txtMessage.value = ""));
}
