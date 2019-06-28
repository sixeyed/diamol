# DIAMOL Chapter 3 Lab - Sample Solution

docker container run -it --name ch03lab diamol/ch03-lab

echo Elton >> ch03.txt 

exit

docker container commit ch03lab ch03-lab-soln
      
docker container run ch03-lab-soln cat ch03.txt

OR 

docker container run ch03-lab-soln cmd /s /c type ch03.txt
