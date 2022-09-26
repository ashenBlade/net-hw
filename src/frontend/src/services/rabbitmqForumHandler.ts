import {ForumHandler, MessageCallback} from "./forumHandler";
import {Message, parseMessage} from "../models/message";
import {AMQPChannel, AMQPConsumer, AMQPQueue, AMQPWebSocketClient} from "@cloudamqp/amqp-client";
import {AMQPBaseClient} from "@cloudamqp/amqp-client/types/amqp-base-client";

export class RabbitmqForumHandler implements ForumHandler {
    amqp: AMQPWebSocketClient
    client?: AMQPBaseClient
    channel?: AMQPChannel
    queue?: AMQPQueue
    consumer?: AMQPConsumer
    consumers: string[]
    callbacks: MessageCallback[]

    constructor(readonly url: string,
                readonly vhost: string = '/',
                readonly username: string = 'guest',
                readonly password: string = 'guest',
                readonly queueName: string = '',
                readonly exchange: string = 'amq.fanout') {
        this.amqp = new AMQPWebSocketClient(url, vhost, 'guest', password);
        this.consumers = [];
        this.callbacks = [];
    }

    async open() {
        this.client = await this.amqp.connect();
        this.channel = await this.client.channel();
        this.queue = await this.channel.queue(this.queueName);
        await this.queue.bind(this.exchange);
        this.consumer = await this.queue.subscribe({noAck: true}, (msg) => {
            const body = msg.bodyToString();
            if (body === null) {
                console.error('Received body is null');
                return;
            }

            let message: Message;
            try {
                message = parseMessage(body);
            } catch (e) {
                console.error('Could not parse received message', e)
                return;
            }

            for (const callback of this.callbacks) {
                try {
                    callback(message);
                } catch (e) {
                    console.error('Error occurred in callback')
                }
            }
        })
    }

    private async resetSubscribers() {
        if (this.queue) {
            for (const consumer of this.consumers) {
                await this.queue.unsubscribe(consumer);
            }
        }
        this.consumers.length = 0;
        this.callbacks.length = 0;
    }

    async close() {
        await this.resetSubscribers();

        if (this.queue) {
            await this.queue.unbind(this.exchange);
        }

        if (this.channel) {
            await this.channel.close()
        }

        if (this.client) {
            await this.client.close();
        }

        if (this.amqp) {
            await this.amqp.close()
        }
    }

    registerOnMessageCallback(cb: MessageCallback): void {
        if (!this.channel) {
            throw new Error('fuck');
        }
        this.callbacks.push(cb);
    }

    async sendMessage(msg: Message): Promise<void> {
        if (!this.queue) {
            throw new Error('Queue is not opened')
        }

        const messageJson = JSON.stringify(msg);
        await this.queue.publish(messageJson)
    }

}