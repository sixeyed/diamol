# DIAMOL Chapter 17 Lab - Sample Solution

Optimizing a Dockerfile which install the Docker CLI.

## Linux version

The original [Dockerfile](linux/Dockerfile) uses Debian and follows Docker's official install instructions. That's all fine but the requirement is just for the Docker CLI, so most of that stuff isn't needed - the unoptimized version is 970MB.

My [Dockerfile.optimized](linux/Dockerfile.optimized) ignores the original. It's a multi-stage build which uses Alpine Linux and installs just the Docker CLI package. The final stage just copies the Docker CLI binary - it builds to 75.3MB.

## Windows version

The original [Dockerfile](windows/Dockerfile) uses Windows Server Core, installs Chocolatey and uses Chocolatey to install Docker. That's the full Docker package which isn't needed, and it comes to 4.95GB.

My [Dockerfile.optimized](linux/Dockerfile.optimized) uses the original Dockerfile as the first part of a multi-stage build. THe final part is based on Nano Server and copies just the Docker CLI binary (and a Windows DLL which is needed - you'll see that from an error message if you try to use Docker without it). It's 320MB.
