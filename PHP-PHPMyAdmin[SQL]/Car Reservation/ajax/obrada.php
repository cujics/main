<?php
    $odgovor['uspeh']="";
    $odgovor['greska']="";
    $db = mysqli_connect("localhost","root","","kola");
    if(isset($_GET['tip']))
    {
        $tip = $_GET['tip'];
        if(isset($_POST['marka']) and $tip == "ubaci_marku")
        {
            $marka = $_POST['marka'];
            $upit = "INSERT INTO marke (naziv) VALUES ('{$marka}');";
            mysqli_query($db,$upit);
            $odgovor['uspeh']="1";
            echo json_encode($odgovor,256); 
        }
        else if($tip == "lista_marki")
        {
            $upit = "SELECT * FROM marke";
            $rez = mysqli_query($db,$upit);
            $sve = mysqli_fetch_all($rez,MYSQLI_ASSOC);
            $odgovor['uspeh']=$sve;
            echo json_encode($odgovor,256);
        }
        else if($tip == "brisi_id" and isset($_POST['id']))
        {
            $id = $_POST['id'];
            $upit = "DELETE FROM marke WHERE id={$id};";
            mysqli_query($db,$upit);
            if(mysqli_error($db))$odgovor['greska']=1;
            echo json_encode($odgovor,256);
        }
        else if($tip == "rezz" and strtotime($_POST['datum'])>time() and isset($_POST['ime']) and isset($_POST['prezime']) and isset($_POST['tel']) and isset($_POST['vrsta']) and isset($_POST['datum'])) 
        {
            $ime = $_POST['ime'];
            $prezime = $_POST['prezime'];
            $tel = $_POST['tel'];
            $vrsta = $_POST['vrsta'];
            $datum = $_POST['datum'];
            //$datum = strtotime($datum);

            $upit = "INSERT INTO rezervacije (Ime,Prezime,Tel,idV,dtm) VALUES ('{$ime}','{$prezime}','{$tel}',{$vrsta},'{$datum}');";
            mysqli_query($db,$upit);
            if(mysqli_error($db))$odgovor['greska']=mysqli_error($db);
            echo json_encode($odgovor,256); 
        }
        else if($tip == "poimenu" and isset($_POST['ime1']))
        {
            $ime1 = $_POST['ime1'];
            $upit = "SELECT * FROM vwrezervacije WHERE Ime LIKE '%{$ime1}%';";
            $rez = mysqli_query($db,$upit);
            if(mysqli_error($db)){$odgovor['greska']="1".mysqli_error($db);echo json_encode($odgovor,256);exit();}
            $sve = mysqli_fetch_all($rez,MYSQLI_ASSOC);
            $odgovor['uspeh']=$sve;
            echo json_encode($odgovor,256); 
        }
        else if($tip == "sver")
        {
            $upit = "SELECT * FROM vwrezervacije";
            $rez = mysqli_query($db,$upit);
            if(mysqli_error($db)){$odgovor['greska']="1".mysqli_error($db);echo json_encode($odgovor,256);exit();}
            $sve = mysqli_fetch_all($rez,MYSQLI_ASSOC);
            $odgovor['uspeh']=$sve;
            echo json_encode($odgovor,256); 
        }
        else if($tip == "povrsti" and isset($_POST['vrsta1']))
        {
            $vrsta1 = $_POST['vrsta1'];
            $upit = "SELECT * FROM vwrezervacije WHERE idV LIKE '%{$vrsta1}%';";
            $rez = mysqli_query($db,$upit);
            if(mysqli_error($db)){$odgovor['greska']="1".mysqli_error($db);echo json_encode($odgovor,256);exit();}
            $sve = mysqli_fetch_all($rez,MYSQLI_ASSOC);
            $odgovor['uspeh']=$sve;
            echo json_encode($odgovor,256); 
        }
        else
        {
            $odgovor['greska']=403;
            echo json_encode($odgovor,256);
        }
    }
    else 
    {
        $odgovor['greska']=404;
        echo json_encode($odgovor,256);
    }
    //$odgovor['uspeh']="bravo".$_GET['tip'];
    //echo json_encode($odgovor,256);
    
?>