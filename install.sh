#!/bin/bash
# Install go, git and nginx server
sudo apt update
sudo apt install -y golang git nginx

# Check the status of the nginx services
sudo systemctl status nginx