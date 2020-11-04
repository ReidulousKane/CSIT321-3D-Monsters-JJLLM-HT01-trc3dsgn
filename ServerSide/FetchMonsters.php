<?php
	$db = mysqli_connect("localhost:3306", "joshuagr", "legoguitarclayrobot") or die("Could not connect: " . mysqli_error($db));
    mysqli_select_db($db, "joshuagr_3DMonsters") or die("Could not select database: joshuagr_3DMonsters");

    $secretKey = "gamesvideoyeahfuck";

    $User = $_POST['User'];
    $Profile = $_POST['Profile'];
    $hash = $_POST['hash'];

    $real_hash = md5($User . $Profile . $secretKey);
    if($real_hash == $hash)
    {
    	$query = "SELECT Monster FROM MonsterCreations WHERE User = \"$User\" AND Profile = \"$Profile\"";
    	$result = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));

    	$num_results = mysqli_num_rows($result);
        
    	for($i = 0; $i < $num_results; $i++)
    	{
    		$row = mysqli_fetch_row($result);
    		echo $row[0] . "\t";
    	}
    	echo "END!";
    }