# DIAMOL Chapter 3 Lab - Sample Solution

Run the container in interactive mode:
```bash
docker container run -it --name ch03lab diamol/ch03-lab
```
Append your name to the text file:
```bash
echo Elton >> ch03.txt 
```
Exit the interactive mode:
```bash
exit
```
Commit your change to the container:
```bash
docker container commit ch03lab ch03-lab-soln
```
See your applied changes on the file in the container:
```bash
docker container run ch03-lab-soln cat ch03.txt
```
or
```bash
docker container run ch03-lab-soln cmd /s /c type ch03.txt
```
