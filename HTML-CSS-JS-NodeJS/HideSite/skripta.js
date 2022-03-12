function f()
{
    
    var p =document.getElementById("forma2");
    if(p.style.display=="none")p.style.display="flex";
    else p.style.display="none";
}
function s()
{
    var p = document.forms["forma2"]["ime"];
    var c = document.forms["forma2"]["prezime"];
    if(p.value.length==0 || c.value.length==0)
    {
        alert("Popunite formu!");
    }
    else 
    {
        p.value="";
        c.value="";
        alert("Vaša prijava je poslata!\nHvala!");
    }
}