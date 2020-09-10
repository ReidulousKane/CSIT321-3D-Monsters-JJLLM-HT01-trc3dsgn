<?php
	$db = mysqli_connect("localhost", "root", "root") or die("Could not connect: " . mysqli_error());
    mysqli_select_db($db, "test") or die("Could not select database: test");

    // Strings must be escaped to prevent SQL injection attack.
    $testUserName =  $_POST['testUserName'];
    $testEmail = $_POST['testEmail'];
    $testPwordHash = $_POST['testPwordHash'];
    $hash = $_POST['hash'];

    $secretKey="gamesvideoyeahfuck";

    $real_hash = md5($testUserName . $testEmail . $testPwordHash . $secretKey);
    if($real_hash == $hash) {
        // Send variables for the MySQL database.
        $testString = mysqli_real_escape_string($db, $testString);
        $query = "INSERT INTO testUserRegistrar (testUsername, testEmail, testPHash) VALUES (\"$testUserName\", \"$testEmail\", \"$testPwordHash\");";
        $result = mysqli_query($db, $query) or die("Query failed: " . mysqli_error($db));
        echo $testUserName . "   " . $testEmail . "   " . $testPwordHash . "   " . $hash . "   " . $real_hash;
    }