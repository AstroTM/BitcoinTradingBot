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

    command = 'INSERT INTO prices VALUES (' + \
        str(date) + ', ' + \
        str(price) + ', ' + \
        str(v_bid) + ', ' + \
        str(v_ask) + ', ' + \
        str(float(ticker[0])) + ', ' + \ # bid
        str(float(ticker[1])) + ', ' + \ # bid_size
        str(float(ticker[2])) + ', ' + \ # ask
        str(float(ticker[3])) + ', ' + \ # ask_size
        str(float(ticker[5])) + ', ' + \ # change_perc
        str(float(ticker[8])) + ', ' + \ # high
        str(float(ticker[9])) + ', ' + \ # low
        str(float(ticker[7])) + ');'     # volume
    c.execute(command)
    #print(date, price, v_bid, v_ask)

    print('Inserted price: ' + str(price) + ' at time: ' + str(date))

    # Save (commit) the changes
    conn.commit()

def main():
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