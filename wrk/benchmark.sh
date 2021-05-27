#!/bin/bash 

URL='http://192.168.1.159:5000/customers?sql=true'
RESULTFILE='results_sql.txt'


echo "warmup" >> $RESULTFILE

wrk -t1 -c1 -d5s  --latency $URL -H 'Accept: application/json' >> $RESULTFILE

echo "6 threads 6 connections 10s" >> $RESULTFILE

wrk -t6 -c6 -d10s --latency $URL -H 'Accept: application/json'  >> $RESULTFILE

echo "12 threads 50 connections 10s" >> $RESULTFILE

wrk -t12 -c50 -d10s --latency $URL -H 'Accept: application/json'  >> $RESULTFILE

echo "12 threads 400 connections 10s" >> $RESULTFILE

wrk -t12 -c400 -d10s --latency $URL -H 'Accept: application/json'  >> $RESULTFILE

sleep 10

URL='http://192.168.1.159:5000/customers?sql=false'
RESULTFILE='results_nosql.txt'

echo "warmup" >> $RESULTFILE

wrk -t1 -c1 -d5s  --latency $URL -H 'Accept: application/json' >> $RESULTFILE

echo "6 threads 6 connections 10s" >> $RESULTFILE

wrk -t6 -c6 -d10s --latency $URL -H 'Accept: application/json'  >> $RESULTFILE

echo "12 threads 50 connections 10s" >> $RESULTFILE

wrk -t12 -c50 -d10s --latency $URL -H 'Accept: application/json'  >> $RESULTFILE

echo "12 threads 400 connections 10s" >> $RESULTFILE

wrk -t12 -c400 -d10s --latency $URL -H 'Accept: application/json'  >> $RESULTFILE