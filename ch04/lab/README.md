# DIAMOL Chapter 4 Lab - Sample Solution

docker image build -t ch04-lab .

docker container run -d -p 804:80 ch04-lab

echo Elton >> ch03.txt 

exit

docker container commit ch03lab ch03-lab-soln
      
docker container run ch03-lab-soln cat ch03.txt

OR 

docker container run ch03-lab-soln cmd /s /c type ch03.txt
