<?php
    $data_set = [ ['Branko','Jovičić','1508993756123','064558372','vs','Beograd','Microsoft Belgrade','a'],
    ['Branko','Julić','1508985751562','064124898','vss','Zaječar','Razvojni centar Nordeus','g'],
    ['Željko','Mitić','0406974789521','061552398','ss','Loznica','Firma sokova Sok','b'],
    ['Srđan','Mijković','2312956756123','062468896','vsk','Beograd','Matijević d.o.o.','ce'],
    ['Đorđe','Davidović','2910999755564','061258665','kks','Niš','Microsoft Belgrade','b'],
    ['Srboje','Jeremić','1711988752315','060556332','ks','Beograd','Plec Wu k.n.','b'],
    ['Slobodan','Elić','2309001755621','0605113712','vs','Novi Sad','Tunel ekspres Šine','d'],
    ['Žtrvorad','Ilijić','2903956789212','064558372','ks','Beograd','Nis','e'],
    ['Branko','Jović','0112999798742','065896124','vss','Sremski Karlovci','Powerade d.o.o.','a'],
    ['Milica','Nikolić','0209989798456','061245373','vs','Beograd','Ubisoft Kragujevac','a']
];
    $con = new mysqli("localhost","root","","system");
    if($_POST['no_rows']==""){header('Location:adbg.php');exit();}
    $brk = $_POST['no_rows'];
    if($_POST['id_db']!="")
    $f = fopen($_POST['id_db'].".txt",'w');
    else $f = fopen(microtime(true).".txt",'w');
    for($i = 0;$i < $brk;$i++)
    {
        for($j = 0;$j < 8;$j++){fwrite($f,$data_set[mt_rand(0,9)][$j]);fwrite($f," ");}
        $query = "INSERT INTO korisnici(ime,prezime,jmbg,tel,kval,mesto,firma,zahtev) VALUES ('{$data_set[mt_rand(0,9)][0]}','{$data_set[mt_rand(0,9)][1]}','{$data_set[mt_rand(0,9)][2]}',
        '{$data_set[mt_rand(0,9)][3]}','{$data_set[mt_rand(0,9)][4]}','{$data_set[mt_rand(0,9)][5]}','{$data_set[mt_rand(0,9)][6]}','{$data_set[mt_rand(0,9)][7]}')";
        
        mysqli_query($con,$query);
        fwrite($f,"\n");
    }
    fclose($f);
    $con->close();
    header('Location:adbg.php');
?>