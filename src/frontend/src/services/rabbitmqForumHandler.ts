import {ForumHandler, MessageCallback} from "./forumHandler";
import {Message, parseMessage} from "../models/message";
import {AMQPChannel, AMQPConsumer, AMQPQueue, AMQPWebSocketClient} from "@cloudamqp/amqp-client";
import {AMQPBaseClient} from "@cloudamqp/amqp-client/types/amqp-base-client";

export class RabbitmqForumHandler implements ForumHandler {
    amqp?: AMQPWebSocketClient
    client?: AMQPBaseClient
    channel?: AMQPChannel
    queue?: AMQPQueue
    consumer?: AMQPConsumer
    callbacks: MessageCallback[]

    constructor(readonly url: string,
                readonly vhost: string = '/',
                readonly username: string = 'guest',
                readonly password: string = 'guest',
                readonly queueName: string = '',
                readonly exchange: string = 'amq.fanout',
                readonly routingKey: string = '') {
        this.callbacks = [];
    }

    async open() {
        this.amqp = new AMQPWebSocketClient(this.url, this.vhost, this.username, this.password);
        this.client = await this.amqp.connect();
        this.channel = await this.client.channel();
        this.queue = await this.channel.queue(this.queueName);
        await this.queue.bind(this.exchange, this.routingKey);
        this.consumer = await this.queue.subscribe({}, (msg) => {
            const body = msg.bodyToString();
            if (body === null) {
                console.error('Received body is null');
                return;
            }
            let message: Message;
            try {
                message = parseMessage(body);
            } catch (e) {
                console.error('Could not parse received message', e);
                return;
            }

            for (const callback of this.callbacks) {
                try {
                    callback(message);
                } catch (e) {
                    console.error('Error occurred in callback');
                }
            }
        });
    }

    private async resetSubscribers() {
        this.callbacks.length = 0;
    }

    async close() {
        await this.resetSubscribers();
        if (this.amqp && !this.amqp.closed) {
            await this.amqp.close()
        }
    }

    registerOnMessageCallback(cb: MessageCallback): void {
        this.callbacks.push(cb);
    }

    async sendMessage(msg: Message): Promise<void> {
        if (!this.channel) {
            throw new Error('Channel is not opened')
        }
        await this.channel.basicPublish(this.exchange, this.routingKey, JSON.stringify(msg));
    }

}