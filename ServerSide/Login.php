<?php
	$db = mysqli_connect("localhost:3306", "joshuagr", "legoguitarclayrobot") or die("Could not connect: " . mysqli_error($db));
    mysqli_select_db($db, "joshuagr_3DMonsters") or die("Could not select database: joshuagr_3DMonsters");

    $secretKey = "gamesvideoyeahfuck";

    $UserName = $_POST['UserName'];
    $PWHash = $_POST['PWHash'];
    $hash = $_POST['hash'];

    $real_hash = md5($UserName . $PWHash . $secretKey);
    if($real_hash == $hash)
    {
    	$UserName = mysqli_real_escape_string($db, $UserName);
	    $query = "SELECT PHash FROM UserRegistrar WHERE Username = \"$UserName\"";
	    $sqlResult = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));
	    $result = mysqli_fetch_row($sqlResult);
	    if ($PWHash == $result[0])
	    {
	    	echo "Successful Login!!!";
	    }
	    else
	    {
	    	echo "Incorrect password!?!";
	    }
    }