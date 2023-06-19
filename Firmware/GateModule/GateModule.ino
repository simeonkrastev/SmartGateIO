#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <SPI.h>
#include <MFRC522.h>
#include "constants.h"

// Represents how the jumper cabels are wired on the board.
#define SS_PIN D4
#define RST_PIN D3

MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;

unsigned long lastTime = 0;
unsigned long timerDelay = 5000;

void setup() {
  // Connect to the USB cable
  Serial.begin(115200);
  Serial.println("Serial ready!");

  // Initialize the RFID sensor
  SPI.begin();
  rfid.PCD_Init();
  Serial.println("RFID ready!");

  // Connect to the local WiFi network
  WiFi.begin(ssid, password);
  Serial.print("WiFi ready! Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
  Serial.println("Hardware ready!");
}

void loop() {
  if((millis() - lastTime) > timerDelay) { //Used to count 5 seconds between each request
    if(WiFi.status() == WL_CONNECTED){

      if( ! rfid.PICC_IsNewCardPresent()) // Check if a card is near the sensor.
        return; // If no card is close - do nothing.

      if( ! rfid.PICC_ReadCardSerial())   // Try to read the card.
        return; // If card cannot be read - do nothing.

      // From this point on the rfid variable stores the current card tag.
      Serial.print("Card read: ");
      // Extract the tag from the rfid variable.
      String tag = "";
      for(byte i = 0; i < 4; i++) {
        tag += rfid.uid.uidByte[i];
      }
      Serial.println(tag);

      // Init the http client to send the tag to the backend.
      WiFiClient client;
      HTTPClient http;
      http.begin(client, serverName);
      http.addHeader("Content-Type", "application/json");

      String requestBody = "\"" + tag + "\""; // The request body needs to be surrounded by quotes.
      int responseCode = http.POST(requestBody);

      if(responseCode > 0) {
        Serial.print("HTTP Response code: ");
        Serial.println(responseCode);
      } else {
        Serial.printf("HTTP error: %s\n", http.errorToString(responseCode).c_str());
      }

      // Close the HTTP connection.
      http.end();
      // Reset the RFID sensor.
      rfid.PICC_HaltA();
      rfid.PCD_StopCrypto1();
    }
    lastTime = millis();
  }

}
