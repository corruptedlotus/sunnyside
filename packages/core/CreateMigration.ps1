param (
	[Parameter(Position = 0, Mandatory = $true)]
	[string] $migrationName
)

dotnet ef migrations add $migrationName --namespace Anovase.Sunnyside.Data.Migrations -o ./Data/Migrations
