<?php
	$db = mysqli_connect("localhost:3306", "joshuagr", "legoguitarclayrobot") or die("Could not connect: " . mysqli_error($db));
    mysqli_select_db($db, "joshuagr_3DMonsters") or die("Could not select database: joshuagr_3DMonsters");

    $secretKey = "gamesvideoyeahfuck";

    // Strings must be escaped to prevent SQL injection attack.
    $UserName =  $_POST['UserName'];
    $Email = $_POST['Email'];
    $PWHash = $_POST['PWHash'];
    $hash = $_POST['hash'];

    $real_hash = md5($UserName . $Email . $PWHash . $secretKey);
    if($real_hash == $hash) {
        // Send variables for the MySQL database.
        $UserName = mysqli_real_escape_string($db, $UserName);
        $Email = mysqli_real_escape_string($db, $Email);
        $query = "INSERT INTO UserRegistrar (Username, Email, PHash) VALUES (\"$UserName\", \"$Email\", \"$PWHash\");";
        $result = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));
        echo "User successfully registered!";
    }