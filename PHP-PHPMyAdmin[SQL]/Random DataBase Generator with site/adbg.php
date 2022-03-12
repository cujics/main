<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ADBG</title>
    <style>
        button{
            background-color:white;
            transition-duration:0.4s;
            border:1px solid black;
        }
        button:hover{
            background-color:#71eb34;
        }
        #container {
        background-image:url('bg.jpg');
        background-repeat:no-repeat;
        background-position:center;
        width:700px;
        height:400px;
        display:flex;
        justify-content:center;
        z-index: -1;
        }
        p{
            text-shadow: -0.7px -0.7px 0 #000, 0.7px -0.7px 0 #000, -0.7px 0.7px 0 #000, 0.7px 0.7px 0 #000;
            font-size:20px;
        }
    </style>
    
</head>
<body style = "background-color:black;color:white;">
    <div id = "glavni" style= "display:flex;justify-content:center;margin-top:300px">
    <div id="containe2r">
    <div>
    <div id="container" style="">
        <form action="adbg_action.php" method="post" style="display: flex;flex-direction:column;;justify-content:center;width:200px;">
            <p style="width=100%;display:flex;justify-content:center;">BROJ KOLONA:&nbsp</p><input type="number" name ="no_rows" style="width=100%;">
            <p style="width=100%;display:flex;justify-content:center;">IME BAZE:&nbsp</p><input type="tedt" name ="id_db">
            <p style="width=100%;display:flex;justify-content:center;"></p>
            <button style="width=100%;">KREIRAJ</button>
        </form>
    </div>
    </div>
</body>
<script>
        let visina = screen.height;
        console.log(visina/2)
        document.getElementById("glavni").style.marginTop = visina/2-200 + "px";
    </script>
</html>