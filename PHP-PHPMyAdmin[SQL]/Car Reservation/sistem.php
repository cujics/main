<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <title>Kontrola</title>
    <style>
        body{
            background-color: black;
        }
        #kont{
            background-color: black;
            display: flex;
            justify-content: center;
        }
        .elem{
            width: 600px;
            min-height: 900px;
            background-color: black;
            margin:5px;
            padding:10px;
            background-image: url("https://images.unsplash.com/photo-1567095751004-aa51a2690368?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8YWJzdHJhY3QlMjBkYXJrfGVufDB8fDB8fA%3D%3D&w=1000&q=80");
            opacity: 0.6;
            display: flex;
            flex-direction: column;
            justify-content: center;
            
        }
        .elem:hover{
            opacity: 1.0;
        }
        .elem2{
            position: absolute;
            top:50%;
            left:50%;
            transform:translate(-50%,50%);
        }
        .elem3
        {
            flex-basis: 100%;
            height: 0;
        }
        p{
            color:white;
            font-weight:600;
            font-size:24px;
        }
        .break{
            flex-basis: 100%;
            height: 0;
        }
        .breakc{
            flex-basis: 100%;
            width: 0;
        }
        .p{
            border:1px solid black;
            background-color: white;
            margin:5px;
            padding: 6px;
        }
    </style>

</head>
<body>
    <div id="kont">

        <div class="elem"><div style="display:flex;justify-content:center;"><p>Ubacivanje u sistem</p> </div><br>
            <input type="text" id="marka" placeholder="Marka auta" style="margin:50px">
            <button id="ubaci" style="margin-left:200px;margin-right:200px;">Ubaci</button> <br> <br>

            <div id="marke" style="margin-left:50px;margin-right:50px;">
            </div>

            <div id="porukam"></div>
            
            <!-- Kod -->

            <script>
                $(function(){
                    osvezi_marke();
                    osvezi_rezervacije();
                   // $("#marke").remove();
                   // $("#marke2").html("<div id='marke'></div>");
                    osvezi_marke();
                    $("#ubaci").click(function(){
                        let marka=$("#marka").val();
                        $.post("ajax/obrada.php?tip=ubaci_marku",{marka:marka},function(data){
                        //console.log("Zdravo:"+data);
                            let odg = JSON.parse(data);
                        if(odg.greska != "")
                        {
                            $("#porukam").html("Greška u obradi zahteva!<br>"+odg.greska).css({"background-color":"red","display":"flex","justify-content":"center"});
                            return false;
                        }
                        osvezi_marke();
                        $("#porukam").html("Uspeh u obradi zahteva!<br>").css({"background-color":"green","display":"flex","justify-content":"center"});
                    })
                    })
                    
                    function osvezi_marke()
                    {
                        $.post("ajax/obrada.php?tip=lista_marki",function(data){
                            let odg = JSON.parse(data);
                            //$("#porukam").html("");
                            if(odg.greska != "")
                            {
                                $("#marke").html("Greška u obradi zahteva!<br>"+odg.greska).css({"background-color":"red","display":"flex","justify-content":"center"});
                                return false;
                            }
                            $("#marke").html("");
                            $(".vrste").html("");
                            for(element of odg.uspeh)
                            {
                                //console.log(element);
                                $(".vrste").append("<option value='"+element.id+"'>"+element.naziv+"</option>");
                                //$("#vrste2").append("<option value='"+element.id+"'>"+element.naziv+"</option>");
                                let p=$("<div style='display:flex;justify-content:center;font-weight:600;'></div>");
                                p.attr("data-id",element.id).attr("class","p");
                                p.attr("id","p");
                                let g=$("<img>");
                                g.attr("src",'https://www.freeiconspng.com/thumbs/recycle-bin-icon/recycle-bin-icon-11.png');
                                g.addClass("bpe");
                                g.attr("data-id",element.id);
                                g.css({"width":"20px","margin-left":"20px"});
                                p.html(""+element.id+" "+element.naziv+" "+element.dk+"");
                                $(p).append(g);
                                //p.html("TEKST");
                                $("#marke").append(p);
                                
                                
                                
                                //$("#marke").append("<div class='p' data-id='"+element.id+"'>"+element.id+" "+element.naziv+" "+element.dk+" "+element.di+" <img class='bpe' data-id = '"+element.id+"' src='https://www.freeiconspng.com/thumbs/recycle-bin-icon/recycle-bin-icon-11.png' height='20px' style='margin-left:20px'></div>");
                            }
                        }
                        )
                    }
                    $("#marke").on("click",".bpe",function(){
                        //console.log("kliknulo");
                        let br = $(this).data("id");
                        //console.log("ID:"+br);
                        $.post("ajax/obrada.php?tip=brisi_id",{id:br},function(data){
                            let odg = JSON.parse(data);
                            $("#porukam").html("");
                            if(odg.greska != "")
                            {
                                $("#porukam").html("Greška u obradi zahteva!<br>").css({"background-color":"red","display":"flex","justify-content":"center"});
                                return false;
                            }
                            osvezi_marke();
                            $("#porukam").html("Uspeh u obradi zahteva brisanja!<br>").css({"background-color":"green","display":"flex","justify-content":"center"});
                        })
                    })
                })
            </script>
        </div>

        <div class="elem"><div style="display:flex;justify-content:center;"><p>Rezervacija auta</p></div> <br>
        
            <input type="text" id="ime" placeholder="Ime" style="margin-left:50px;margin-right:50px;"> <br> <br>
            <input type="text" id="prezime" placeholder="Prezime" style="margin-left:50px;margin-right:50px;"> <br> <br>
            <input type="text" id="tel" placeholder="Telefon" style="margin-left:50px;margin-right:50px;"> <br> <br>
            <select class="vrste" id="vrsta1" style="margin-left:250px;margin-right:250px;">
                <option value="0">-- Izaberi marku auta --</option>
            </select> <br> <br>
            <input type="date" id="datum" style="margin-left:200px;margin-right:200px;"> <br> <br>
            <button id="rezerv" style="margin-left:200px;margin-right:200px;">Rezerviši</button> <br> <br>
            <div id="porukar"></div>
            <script>
                $("#rezerv").click(function(){
                    let ime = $("#ime").val();
                    let prezime = $("#prezime").val();
                    let tel = $("#tel").val();
                    let vrsta = $("#vrsta1").val();
                    let datum = $("#datum").val();
                    console.log(datum+ime);
                    $.post("ajax/obrada.php?tip=rezz",{ime:ime,prezime:prezime,tel:tel,vrsta:vrsta,datum:datum},function(data){
                        let odg = JSON.parse(data);
                        $("#porukar").html("");
                        if(odg.greska != "")
                        {
                             $("#porukar").html("Greška u obradi zahteva!<br>").css({"background-color":"red","display":"flex","justify-content":"center"});
                            return false;
                        }
                        osvezi_rezervacije();
                        $("#porukar").html("Uspeh u obradi zahteva rezervacije!<br>").css({"background-color":"green","display":"flex","justify-content":"center"});
                    })
                })
            </script>

        </div>

        <div class="elem"><div style="display:flex;justify-content:center;"><p>Prikaz rezervacija</p></div> <br>
            <input type="text" id="ajde" placeholder="Ime" style="margin-left:180px;margin-right:180px;"> <br><button id="poimenu" style="margin-left:200px;margin-right:200px;"> Pretrazi po imenu </button><br> <br>
            <select class="vrste" id="vrsta2" style="margin-left:200px;margin-right:200px;">
                <option value="0">-- Izaberi marku auta --</option>
            </select> <br><button id="povrsti" style="margin-left:210px;margin-right:210px;"> Pretrazi po marki </button><br> <br>
            <button id="svep" style="margin-left:200px;margin-right:200px;"> Prikazi sve </button> <br> <br>
            <div id="rezervc">

            </div><br>
            <div id="porukac"></div>
            <script>
                
                $("#poimenu").click(function(){
                    let ime1 = $("#ajde").val();
                    console.log("ime1:"+ime1);
                    $.post("ajax/obrada.php?tip=poimenu",{ime1:ime1},function(data){
                        let odg = JSON.parse(data);
                        $("#porukac").html("");
                        if(odg.greska != "")
                        {
                             $("#porukac").html("Greška u obradi zahteva!<br>").css({"background-color":"red","display":"flex","justify-content":"center"});
                            return false;
                        }
                        $("#rezervc").html("");
                        for(element of odg.uspeh)
                        {
                            $("#rezervc").append("<div style='background-color:white;border:1px solid black;margin:5px;display:flex;justify-content:center;font-weight:600;'>"+element.Ime+" "+element.Prezime+" "+element.Tel+" "+element.dtm+" "+element.naziv+" </div>");
                        }
                        $("#porukac").html("Uspeh u obradi zahteva rezervacije!<br>").css({"background-color":"green","display":"flex","justify-content":"center"});
                    })
                })
                $("#svep").click(function(){osvezi_rezervacije();})
                $("#povrsti").click(function(){
                    let vrsta1 = $("#vrsta2").val();
                    $.post("ajax/obrada.php?tip=povrsti",{vrsta1:vrsta1},function(data){
                        let odg = JSON.parse(data);
                        $("#porukac").html("");
                        if(odg.greska != "")
                        {
                             $("#porukac").html("Greška u obradi zahteva!<br>").css({"background-color":"red","display":"flex","justify-content":"center"});
                            return false;
                        }
                        $("#rezervc").html("");
                        for(element of odg.uspeh)
                        {
                            $("#rezervc").append("<div style='background-color:white;border:1px solid black;margin:5px;display:flex;justify-content:center;font-weight:600;'>"+element.Ime+" "+element.Prezime+" "+element.Tel+" "+element.dtm+" "+element.naziv+" </div>");
                        }
                        $("#porukac").html("Uspeh u obradi zahteva rezervacije!<br>").css({"background-color":"green","display":"flex","justify-content":"center"});
                    })
                })
                function osvezi_rezervacije()
                {
                    $.post("ajax/obrada.php?tip=sver",function(data){
                        let odg = JSON.parse(data);
                        $("#porukac").html("");
                        if(odg.greska != "")
                        {
                             $("#porukac").html("Greška u obradi zahteva!<br>").css({"background-color":"red","display":"flex","justify-content":"center"});
                            return false;
                        }
                        $("#rezervc").html("");
                        for(element of odg.uspeh)
                        {
                            $("#rezervc").append("<div style='background-color:white;border:1px solid black;margin:5px;display:flex;justify-content:center;font-weight:600;'>"+element.Ime+" "+element.Prezime+" "+element.Tel+" "+element.dtm+" "+element.naziv+"</div>");
                        }
                        $("#porukac").html("Uspeh u obradi zahteva rezervacije!<br>").css({"background-color":"green","display":"flex","justify-content":"center"});
                    })
                }
            </script>
        </div>

    </div>
</body>
</html>

<!-- 
    SELECT
    *
FROM
    rezervacije
INNER JOIN marke ON rezervacije.idV = marke.id
            -->