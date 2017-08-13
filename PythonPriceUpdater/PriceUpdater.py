"""Script to gather market data from bitfinex Spot Price API."""
import requests
from pytz import utc
from datetime import datetime
import time
from apscheduler.schedulers.blocking import BlockingScheduler
import sqlite3

def tick():
    conn = sqlite3.connect('../priceData.db')
    c = conn.cursor()
    
    ticker = requests.get('https://api.bitfinex.com/v2/ticker/tETHBTC').json()
    depth = requests.get('https://api.bitfinex.com/v2/trades/tETHBTC/hist').json()
    date = time.time()
    price = float(ticker[6])

    asks = []
    bids = []
        
    for i in range(0, len(depth) - 1):
        if depth[i][1]/1000 < date - 100:
            for j in range(i, len(depth) - 1):
                depth.pop(i)
            break

    for trade in depth:
        if trade[2] < 0:
            asks.append(-trade[2])
        else:
            bids.append(trade[2])

    v_bid = sum([bid for bid in bids])
    v_ask = sum([ask for ask in asks])

"""
    try:
        price_stat = last_price / price
    except UnboundLocalError:
        last_price = price
        price_stat = 1
    price(price_stat)
    if(price_stat <= 0.9):
        expected_result = 0
    elif(price_stat >= 1.1):
        expected_result = 1
    else:
        price_stat = price_stat - 1
        price_stat = price_stat * 5
        expected_result = price_stat + 0.5
"""

    command = 'INSERT INTO prices VALUES (' + \
        str(date) + ', ' + \
        str(price) + ', ' + \
        str(v_bid) + ', ' + \
        str(v_ask) + ', ' + \
        str(float(ticker[0])) + ', ' + \
        str(float(ticker[1])) + ', ' + \
        str(float(ticker[2])) + ', ' + \
        str(float(ticker[3])) + ', ' + \
        str(float(ticker[5])) + ', ' + \
        str(float(ticker[8])) + ', ' + \
        str(float(ticker[9])) + ', ' + \
        str(float(ticker[7])) ');'
        # bid
        # bid_size
        # ask
        # ask_size
        # change_perc
        # high
        # low
        # volume
    c.execute(command)
    #print(date, price, v_bid, v_ask)

    print('Inserted price: ' + str(price) + ' at time: ' + str(date))

    # Save (commit) the changes
    conn.commit()

    last_price = price

def main():
    global last_price

    tick()

    """Run tick() at the interval of every ten seconds."""
    scheduler = BlockingScheduler(timezone=utc)
    scheduler.add_job(tick, 'interval', seconds=10)
    try:
        scheduler.start()
    except (KeyboardInterrupt, SystemExit):
        pass


if __name__ == '__main__':
    try:
        main()
    except KeyboardInterrupt:
        conn.close()
        print('Interrupted')
        sys.exit(0)