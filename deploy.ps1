#$src = "C:\Users\selliott\Desktop\PasswordReset\PasswordResetWeb"
#$dst = "C:\Users\selliott\Desktop\PasswordReset\WebApps.zip"
#$webappsServer = "apps.sociable.social"
#[Reflection.Assembly]::LoadWithPartialName( "System.IO.Compression.FileSystem" )
#[System.IO.Compression.ZipFile]::CreateFromDirectory($src, $dst)

$Username = "Administrator"
$Password = "n-!fK)EF@i"

$WebClient = New-Object System.Net.WebClient
$WebClient.Credentials = New-Object System.Net.NetworkCredential($Username, $Password)

$Dest = "\\52.205.69.119\c$\deploy\WebApps.zip"
$Source   = "c:\Users\selliott\Desktop\PasswordReset\WebApps.zip"

$WebClient.UploadFile($Dest, $Source)

