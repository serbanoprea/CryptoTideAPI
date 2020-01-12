echo $AZURE_STORAGE_KEY
echo $AZURE_STORAGE_ACCOUNT
# wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
# sudo dpkg -i packages-microsoft-prod.deb
# sudo add-apt-repository universe
# sudo apt-get update
# sudo apt-get install apt-transport-https
# sudo apt-get update
# sudo apt-get install dotnet-sdk-3.1

# dotnet --list-sdks
# dotnet --list-runtimes

# curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
# sudo apt-get update
# sudo apt-get install ca-certificates curl apt-transport-https lsb-release gnupg
# curl -sL https://packages.microsoft.com/keys/microsoft.asc | 
#     gpg --dearmor | 
#     sudo tee /etc/apt/trusted.gpg.d/microsoft.asc.gpg > /dev/null

# AZ_REPO=$(lsb_release -cs)
# echo "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ $AZ_REPO main" | 
#     sudo tee /etc/apt/sources.list.d/azure-cli.list

# sudo apt-get update
# sudo apt-get install azure-cli

# az --version

# cd ./CryptoTideAPI
# sudo AZURE_STORAGE_KEY=$AZURE_STORAGE_KEY AZURE_STORAGE_ACCOUNT=$AZURE_STORAGE_ACCOUNT az storage blob download --container-name "settings" --name "CryptoTideAPIConfig/appsettings.json" --file "appsettings.json"
# cd ..
