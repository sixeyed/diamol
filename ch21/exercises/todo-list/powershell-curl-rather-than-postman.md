    
    Hi Elton,
    
    I was simarly stymed by the curl on my windows, powershell cli - 
     it was aliased to Invoke-RestMethod, so the flags and method that worked for me was 

    ```PowerShell
    curl -Method 'Post' -Uri 'http://localhost:8081/todo' -ContentType 'application/json' -Body (@{ Item = "curl on windows PowerShell can be mapped to Invoke-RestMethod or 'irm'" } | ConvertTo-Json);

    Invoke-RestMethod -Method 'Post' -Uri 'http://localhost:8081/todo' -ContentType 'application/json' -Body (@{ Item = "curl on windows PowerShell can be mapped to Invoke-RestMethod or 'irm'" } | ConvertTo-Json);
    
    irm -Method 'Post' -Uri 'http://localhost:8081/todo' -ContentType 'application/json' -Body (@{ Item = "curl on windows PowerShell can be mapped to Invoke-RestMethod or 'irm'" } | ConvertTo-Json);

    ```


    Cheers!

    Grant