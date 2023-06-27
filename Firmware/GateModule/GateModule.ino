#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <SPI.h>
#include <MFRC522.h>
#include "constants.h"
#include <Arduino_JSON.h>

// Represents how the jumper cabels are wired on the board.
#define SS_PIN D1
#define RST_PIN D0

MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;

unsigned long lastTime = 0;
unsigned long timerDelay = 5000;

int Led1 = D8;

/*RGBCOLOUR CONST*/

const int red_pin = D3;
const int green_pin = D2;
const int blue_pin = D4;

void SetLedPower(int ledPower){
  if(ledPower == 0){
    
    analogWrite(Led1, 0);
    
  }
  if(ledPower == 1){
   analogWrite(Led1, 19);
   
  }
  if(ledPower == 2){
   
    analogWrite(Led1, 255);

  
  }
}
void setup() {
  pinMode(red_pin, OUTPUT);
  pinMode(green_pin, OUTPUT);
  pinMode(blue_pin, OUTPUT);
  pinMode(Led1, OUTPUT);
  
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
       SetLedPower(1);
      if(  !rfid.PICC_IsNewCardPresent())

      // Check if a card is near the sensor.
        return; // If no card is close - do nothing.
      
      if( !rfid.PICC_ReadCardSerial())   // Try to read the card.
        return;
        SetLedPower(2);
         // If card cannot be read - do nothing.
      
      String tag = ReadTag();
    
      String response = SendRequest (tag);
        
        JSONVar jsonResp = JSON.parse(response);
  
        bool validation = jsonResp["validation"];
      
        if(validation == true){
          Serial.println("Card valid");
         rgbColor("green");
         
       }
       else if( validation == false){
         Serial.println("Card Invalid");
          rgbColor("red");
         
       }
       else{
         rgbColor("off");
       }
      // Reset the RFID sensor.
      rfid.PICC_HaltA();
      rfid.PCD_StopCrypto1();
    }
    lastTime = millis();
  }

}
String ReadTag()
{
  // From this point on the rfid variable stores the current card tag.
      Serial.print("Card read: ");
      // Extract the tag from the rfid variable.
      String tag = "";
      for(byte i = 0; i < 4; i++) {
        tag += rfid.uid.uidByte[i];
      }
      Serial.println(tag);
      return tag;
}

String SendRequest(String tag)
{
  // Init the http client to send the tag to the backend.
  WiFiClient client;
  HTTPClient http;
  http.begin(client, serverName);
  http.addHeader("Content-Type", "application/json");

  String requestBody = "\"" + tag + "\""; // The request body needs to be surrounded by quotes.
  int responseCode = http.POST(requestBody);
  Serial.println(http.getString());
  if(responseCode > 0) {
    Serial.print("HTTP Response code: ");
    Serial.println(responseCode);
  } 
  else {
    Serial.printf("HTTP error: %s\n", http.errorToString(responseCode).c_str());
  }
  String result = http.getString();
  // Close the HTTP connection.
  http.end();
    return result;
}
void rgbColor(String color)
{

  if(color == "red"){
    digitalWrite(red_pin, HIGH);
    digitalWrite(blue_pin, LOW);
    digitalWrite(green_pin, LOW);
    delay(20);
  } 
  else if (color == "green")
  {
    digitalWrite(red_pin, LOW);
    digitalWrite(blue_pin, LOW);
    digitalWrite(green_pin, HIGH);
    delay(20);
  }

  else if (color == "off")
  {
    digitalWrite(green_pin, LOW);
    digitalWrite(blue_pin, LOW);
    digitalWrite(red_pin, LOW);

  }
}



