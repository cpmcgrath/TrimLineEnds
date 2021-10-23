function Replace($file, $before, $after)
{
    $content = Get-Content $file | Foreach-Object {$_ -replace $before, $after }
    $content | Set-Content $file -Encoding UTF8
}

function UpdateVersion($project, $version)
{
    $file = "./$project/Properties/AssemblyInfo.cs"
    Replace $file "AssemblyVersion\s*\([^\)]+\)"     "AssemblyVersion    (`"$version`")"
    Replace $file "AssemblyFileVersion\s*\([^\)]+\)" "AssemblyFileVersion(`"$version`")"
}

function UpdateVsVersion($project, $version)
{
    UpdateVersion $project $version
        
    Replace "./$project/Package.cs" `
            "\[InstalledProductRegistration\(`"#110`", `"#112`", `"[^`"]+`", IconResourceID = 400\)\]" `
            "[InstalledProductRegistration(`"#110`", `"#112`", `"$version`", IconResourceID = 400)]"
        
    Replace "./$project/source.extension.vsixmanifest" `
            "370b7457-7f7a-42ab-8795-8e2faa714cc6`" Version=`"[^`"]+`"" `
            "370b7457-7f7a-42ab-8795-8e2faa714cc6`" Version=`"$version`""
}