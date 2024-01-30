# install
dotnet tool install --global dotnet-ef --version 8.*
# add dev migration
dotnet ef migrations add InitialCreate --context MigrationAppDbContext