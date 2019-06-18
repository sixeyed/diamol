@echo off

echo ---------------------
echo Hello from Chapter 2!
echo ---------------------
echo My name is:
hostname
echo ---------------------
echo I'm running on:
ver
echo ---------------------
echo My address is:
ipconfig | findstr "IPv4"
echo ---------------------