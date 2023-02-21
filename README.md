# AzureServiceBusSender


<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#prerequisites">Getting Started</a>
      <ul>
        <li><a href="#Azure">Azure</a></li>
        <li><a href="#SendGrid">SendGrid</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!--  Azure  -->
## Azure
### Create Service Bus Queue- https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-portal
### Note: Connection Strings of Queues Policies should be dedicated respectively for Sender and Listeners.
### Create two separate Policy, one for AzureServiceBusSender with Send only, and Listen only AzureServiceBusReceiver.
### Variables
[SenderAzureServiceBus] - Connection Strings of sender
[ReceiverAzureServiceBus] - Connection Strings of receiver
[SericeBus_QueueName] - Name of queue create Service Bus
[SendGrid_API_KEY] - Name of queue create Service Bus
## SendGrid
### Create Account and API Key- https://docs.sendgrid.com/api-reference/api-keys/create-api-keys

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.
<!-- USAGE -->
## Usage
### Receiver
![Alt text](/README/ReceiverDashboard.png?raw=true "Receiver")
### Scanner
![Alt text](/README/SenderDashboard.png?raw=true "Sender")
### Result
![Alt text](/README/PostSubmission.png?raw=true "Result")

