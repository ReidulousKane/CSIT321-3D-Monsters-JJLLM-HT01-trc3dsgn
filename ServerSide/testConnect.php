<?php
	$db = mysqli_connect("localhost", "root", "root") or die("Could not connect: " . mysqli_error());
    mysqli_select_db($db, "test") or die("Could not select database: test");

    // Strings must be escaped to prevent SQL injection attack.
    $testString =  $_POST['testString'];
    $hash = $_POST['hash'];

    $secretKey="gamesvideoyeahfuck";

    $real_hash = md5($testString . $secretKey);
    echo $testString . "   " . $hash . "   " . $real_hash;
    if($real_hash == $hash) {
        // Send variables for the MySQL database class.
        $testString = mysqli_real_escape_string($db, $testString);
        $query = "INSERT INTO testConnection (testString) VALUES (\"$testString\");";
        $result = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));
        echo "\nServer received: $testString";
    }
