<?php
	$db = mysqli_connect("localhost", "root", "root") or die("Could not connect: " . mysqli_error());
    mysqli_select_db($db, "test") or die("Could not select database: test");

    $testUsername = $_POST['testUserName'];
    $testPWHash = $_POST['testPWHash'];
    $hash = $_POST['hash'];

	$testString = mysqli_real_escape_string($db, $testUsername);
    $query = "SELECT testPHash FROM testUserRegistrar WHERE testUsername = \"$testUsername\"";
    $sqlResult = mysqli_query($db, $query) or die("Query failed: " . mysql_error());
    $result = mysqli_fetch_row($sqlResult);
    if ($testPWHash == $result[0])
    {
    	echo "Successful Login!!!";
    }
    else
    {
    	echo "Incorrect password!?!";
    }